(function () {
    angular.module("main").component("view", {
        bindings: {
            scoreboard: "<"
        },
        controllerAs: "vm",
        controller: ["scoreboardResource", "$state", "$timeout", function (scoreboardResource, $state, $timeout) {
            var vm = this;
            vm.$onInit = function () {
                console.log(vm.scoreboard);
            }

            vm.returnToList = function () {
                $state.go("scoreboards");
            };

            vm.save = function () {
                
            };
        }],
        templateUrl: "app/scoreboard/create/view.template.html"
    });
}());