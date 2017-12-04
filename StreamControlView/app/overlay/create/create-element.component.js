(function () {
    angular.module("main").component("element", {
        bindings: {
            overlay: "<"
        },
        controllerAs: "vm",
        controller: ["overlayResource", "$state", "$timeout", function (overlayResource, $state, $timeout) {
            var vm = this;
            vm.$onInit = function () {
                vm.elements = [];
                vm.players = [];
            }

            vm.addelement = function () {
                vm.elements.push({
                    Label: ""
                });
            }

            vm.addPlayer = function () {
                vm.players.push({
                    Label: ""
                });
            }

            vm.returnToList = function () {
                $state.go("overlays");
            };

            vm.save = function () {
                vm.overlay.Players = vm.players;
                vm.overlay.elements = vm.elements;
                $state.go("overlayCreate.view", { overlay: vm.overlay });
            };
        }],
        templateUrl: "app/overlay/create/element.template.html"
    });
}());