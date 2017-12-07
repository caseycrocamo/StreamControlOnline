(function () {
    angular.module("main").component("navbar", {
        controllerAs: "vm",
        controller: ["$location","authService", "$state", function ($location, authService, $state) {
            var vm = this;
            authService.fillAuthData();
            vm.user = authService.authentication;

            vm.$onInit = function () {
                let stateName = $state.current.name.split('.')[0];
                if (stateName.indexOf('scoreboard') !== -1) {
                    stateName = 'scoreboard';
                }
                switch (stateName) {
                    case "scoreboard":
                        $('#scoreboardsNav').addClass('active');
                        break;
                    default:
                        $('#homeNav').addClass('active');
                }
            };

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