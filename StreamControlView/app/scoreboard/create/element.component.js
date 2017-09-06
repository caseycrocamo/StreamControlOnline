(function () {
    angular.module("main").component("element", {
        bindings: {
            scoreboardId: "<"
        },
        controllerAs: "vm",
        controller: ["scoreboardResource", "$state", "$timeout", function (scoreboardResource, $state, $timeout) {
            var vm = this;
            vm.element = {};
            vm.message = 'loading...';
            vm.elementType = 'Field';
            vm.label = '';

            vm.$onInit = function () {
                vm.nextElement = function () {
                    vm.saveElement();
                    $state.go($state.current, { scoreboardId: vm.scoreboardId }, { reload: true });
                };

                vm.saveElement = function () {
                    vm.message = "saving...";
                    vm.element.Label = vm.label;
                    if (vm.elementType === 'Field') {
                        scoreboardResource.updateField({ id: vm.scoreboardId }, vm.element)
                            .$promise
                            .then(
                            //on success
                            function () {
                                vm.message = "element added";
                            },
                            //on failure
                            function () {
                                vm.message = "creation failed";
                            });
                    }
                    else {
                        scoreboardResource.updatePlayer({ id: vm.scoreboardId }, vm.element)
                            .$promise
                            .then(
                            //on success
                            function () {
                                vm.message = "element added";
                            },
                            //on failure
                            function () {
                                vm.message = "creation failed";
                            });
                    }
                         
                };

                vm.finish = function () {
                    vm.saveElement();
                    $state.go("scoreboards");
                };
            }
        }],
        templateUrl: "app/scoreboard/create/element.template.html"
    });
}());