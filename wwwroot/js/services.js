"use strict";
/**
 * Сервис для работы со списком товаров
 */
storeV.service("productsService", function ($http) {
	//Список доступных товаров
	this.products = {};
    
	var self = this;

	//Получаем список товаров с сервера
	$http.get(products_url).then(function(response) {
		self.products = response.data;
	}, function(response) {
		console.log(response.data || "Request failed");
	});
});

/**
 * Сервис для работы с корзинами
 */
storeV.service("basketService", function (productsService) {

	/**
     * Корзина товаров
     * Для каждой валюты в объект добавляется массив индексов добавленных товаров из списка товаров
	 * Индексы в массиве могут дублироваться, если добавлено несколько одинаковых товаров
     */
	this.basket = {};

	/**
     * Добавление товара в корзину
     * @param {number} product - индекс товара в списке товаров
     */
	this.add = function (product) {
		var currencyBasket = this.basket[productsService.products[product].currency];

		if(undefined===currencyBasket)
			currencyBasket = this.basket[productsService.products[product].currency] = [];

		currencyBasket.push(product);
	};

	/**
     * Проверка наличия товара в корзине
     * @param {number} product - индекс товара в списке товаров
     * @returns {Boolean} true если товар уже добавлен в корзину
     */
	this.has = function (product) {
		var currencyBasket = this.basket[productsService.products[product].currency];

		if(undefined===currencyBasket) return false;

		var index = currencyBasket.indexOf(product);

		return index > -1;
	};

	/**
     * Удаление товара из корзины
     * @param {number} product - индекс товара в списке товаров
     */
	this.remove = function (product) {
		var currencyBasket = this.basket[productsService.products[product].currency];

		var index = currencyBasket.lastIndexOf(product);

		if (index > -1)
		{
			currencyBasket.splice(index, 1);
			if(currencyBasket.length<1)
				delete this.basket[productsService.products[product].currency];
		}
	};

	/**
     * Получает общую стоимость товаров в корзине в указанной валюте
     * @param {String} currency - валюта для которой требуется получить общую стоимость
     * @returns {number} общую стоимость товаров в корзине в указанной валюте
     */
	this.getTotal = function (currency)
	{
		var getSum = function (total, num) {
			return total + productsService.products[num].price;
		};

		return this.basket[currency].reduce(getSum, 0).toFixed(2);
	};

	/**
     * Получает список идентификаторов товаров в корзине в указанной валюте
     * @param {String} currency - валюта для которой требуется получить список товаров
     * @returns {number[]} массив идентификаторов товаров в корзине в указанной валюте
	 *                     идентификаторы в массиве могут дублироваться, если добавлено несколько одинаковых товаров
     */
	this.getProducts = function (currency)
	{
		var getId = function (num) {
			return productsService.products[num].productID;
		};

		return this.basket[currency].map(getId);
	};
});

/**
 * Сервис для работы с заказами
 */
storeV.service("orderService", function (basketService, $http) {

	/**
     * Завершает размещение заказа для корзины в указанной валюте 
     * @param {String} currency - валюта корзины
     */
	var complete = function(data, currency)
	{
		if(data.success)
		{
			alert("Заказ успешно оформлен.");
			delete basketService.basket[currency];
		}
		else
		{
			alert("Не удалось оформить заказ.\n"+data.message);
		}
	};

	/**
     * Подтверждает оплату заказа для корзины в указанной валюте 
     * @param {number} reservId - идентификатор заказа
     * @param {String} currency - валюта корзины
     * @param {Object} payment - данные платежа, полученный от платежного сервиса
     */
	var confirm = function(reservId, currency, payment)
	{
		console.log("Отправка квитанции на сервер...");
        
		$http.put(reservations_url+"/"+reservId, payment)
			.then(function(response) {
				complete(response.data, currency);
			}, function(response) {
				console.log(response.data || "Request failed");
				alert("Не удалось отправить квитанцию на сервер.");
			});
	};

	/**
     * Отменяет заказ 
     * @param {number} reservId - идентификатор заказа
     */
	var cancel = function(reservId)
	{
		$http.delete(reservations_url+"/"+reservId)
			.then(function(response) {
				console.log("Резерв отменен.");
			}, function(response) {
				console.log("Не удалось отправить отмену резерва на сервер.");
				console.log(response.data || "Request failed");
			});
	};

	/**
     * Осуществляет оплату заказа для корзины в указанной валюте 
     * @param {number} reservId - идентификатор заказа
     * @param {String} currency - валюта корзины
     */
	var pay = function(reservId, currency)
	{
		var amount = Number(basketService.getTotal(currency));
		var widget = new cp.CloudPayments();
		widget.charge({ // options
			publicId: "test_api_00000000000000000000001",  //id из личного кабинета
			description: "Пример оплаты (деньги сниматься не будут)", //назначение
			amount: amount,
			currency: currency,
			invoiceId: reservId.toString()
		},
		function (options) { // success 
			//действие при успешной оплате
			confirm(reservId, currency, options);
		},
		function (reason, options) { // fail
			//действие при неуспешной оплате
			alert("Не удалось произвести оплату!");
			cancel(reservId);
		}); 
	};
     
	/**
     * Размещает заказ для корзины в указанной валюте 
     * @param {String} currency - валюта корзины
     */
	this.place = function (currency) {
		$http.post(reservations_url, basketService.getProducts(currency))
			.then(function(response) {
				if(response.data)
					pay(response.data, currency);
			}, function(response) {
				console.log(response.data || "Request failed");
				alert("Не удалось зарезервировать товары.");
			});
	};
});