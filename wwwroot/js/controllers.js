"use strict";

// Регистрация контроллера storeCtrl для модуля storeV
storeV.controller("storeCtrl", function ($scope, $http, productsService, basketService, orderService) {

	$scope.basket = basketService.basket;

	$scope.getProducts = function() {return productsService.products;};

	$scope.addToBasket = function (product) {
		return basketService.add(product);
	};

	$scope.isInBasket = function (product) {
		return basketService.has(product);
	};

	$scope.removeFromBasket = function (product) {
		return basketService.remove(product);
	};

	$scope.purchase = function (currency) {
		return orderService.place(currency);
	};

	$scope.getTotal = basketService.getTotal;
});