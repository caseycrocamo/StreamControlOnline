(function () {
    angular.module("main").component("element", {
        bindings: {
            scoreboard: "<"
        },
        controllerAs: "vm",
        controller: ["scoreboardResource", "$state", "$timeout", function (scoreboardResource, $state, $timeout) {
            var vm = this;
            vm.$onInit = function () {
                vm.textElements = [];
                vm.playerElements = [];
            }

            vm.addTextElement = function () {
                vm.textElements.push({
                    Label: ""
                });
            }

            vm.addPlayerElement = function () {
                vm.playerElements.push({
                    Label: ""
                });
            }

            vm.returnToList = function () {
                $state.go("scoreboards");
            };

            vm.save = function () {
                vm.scoreboard.PlayerElements = vm.playerElements;
                vm.scoreboard.TextElements = vm.textElements;
                $state.go("scoreboardCreate.view", { scoreboard: vm.scoreboard });
            };
        }],
        templateUrl: "app/scoreboard/create/element.template.html"
    });
}());