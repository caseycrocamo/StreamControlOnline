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

            $(".nav li").click(function () {
                $(".nav li").find(".active").removeClass("active");
                $(this).parent().addClass("active");
            });
        }],
        templateUrl: "app/nav/navbar.template.html"
    });
}());