(function () {
    angular.module("main").component("scoreboardView",
        {
            bindings: {
                scoreboardId: "<",
                viewId: "<"
            },
            controllerAs: "vm",
            controller: ["scoreboardResource","authService", "$scope", function (scoreboardResource, authService, $scope) {
                var vm = this;
                vm.scoreboard = null;
                vm.message = "loading...";
                vm.$onInit = function () {
                    scoreboardResource.get({ id: vm.scoreboardId })
                        .$promise
                        .then(function (data) {
                            vm.message = "";
                            vm.scoreboard = data;
                            for (let i = 0; i < vm.scoreboard.Views.length; i++) {
                                if (vm.scoreboard.Views[i].ViewID == vm.viewId) {
                                    vm.view = vm.scoreboard.Views[i];
                                }
                            };
                        })
                };

                $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
                    for (let i = 0; i < vm.view.Style.length; i++) {
                        let cssString = "";
                        angular.forEach(vm.view.Style[i], function (value, key) {
                            if (key != "StyleID" && key != "DivID" && value != null) {
                                cssString += key + ":" + value + ";";
                            }
                        });
                        console.log(cssString);
                        document.getElementById(vm.view.Style[i].DivID).style.cssText = cssString;
                    }
                });
            }],
            templateUrl: "app/scoreboard/scoreboard-view.template.html"
        });
}());