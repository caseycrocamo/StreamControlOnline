(function () {
    angular.module("main").component("element", {
        bindings: {
            scoreboard: "<"
        },
        controllerAs: "vm",
        controller: ["scoreboardResource", "$state", "$timeout", function (scoreboardResource, $state, $timeout) {
            var vm = this;
            vm.$onInit = function () {
                vm.name = vm.scoreboard.Name;
            }
        }],
        templateUrl: "app/scoreboard/create/element.template.html"
    });
}());