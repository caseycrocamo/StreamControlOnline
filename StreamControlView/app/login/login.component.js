(function () {
    angular.module("main").component("login", {
        controllerAs: "vm",
        controller: ["$location", "authService", function ($location, authService) {
            var vm = this;
            vm.message = "";
            vm.userData = {
                userName: "",
                password: "",
            };

            vm.login = function () {
                authService.login(vm.userData).then(function (response) {
                    $location.path('');

                }, function (err) {
                    vm.message = err.error_description;
                });
            };
        }],
        templateUrl: "app/login/login.template.html"
    });
}());