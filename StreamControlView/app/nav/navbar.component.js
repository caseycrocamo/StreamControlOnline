(function () {
    angular.module("main").component("navbar", {
        controllerAs: "vm",
        controller: ["$location","authService", function ($location, authService) {
            var vm = this;
            authService.fillAuthData();
            vm.user = authService.authentication;

            vm.isActive = function (viewLocation) {
                return viewLocation === $location.path();
            };

            vm.logout = function () {
                authService.logOut();
                location.reload();
            };
        }],
        templateUrl: "app/nav/navbar.template.html"
    });
}());