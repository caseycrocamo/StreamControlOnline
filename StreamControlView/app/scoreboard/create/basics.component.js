(function () {
    angular.module("main").component("basics", {
        controllerAs: "vm",
        controller: ["scoreboardResource", "$state", "$timeout", "authService", function (scoreboardResource, $state, $timeout, authService) {
            var vm = this;
            vm.scoreboard = {
                Name : '',
                OwnerId : authService.authentication.userName
            };
            vm.name = "";
            vm.message = 'loading...';

            vm.$onInit = function () {
                vm.returnToList = function () {
                    $state.go("scoreboards");
                };

                vm.save = function () {
                    vm.message = "saving...";
                    vm.scoreboard.Name = vm.name;
                    
                    $state.go("scoreboardCreate.element", { scoreboard: vm.scoreboard });
                };

            }
        }],
        templateUrl: "app/scoreboard/create/basics.template.html"
    });
}());