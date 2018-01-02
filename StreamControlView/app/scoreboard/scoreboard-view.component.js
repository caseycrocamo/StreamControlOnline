(function () {
    angular.module("main").component("scoreboardView",
        {
            bindings: {
                scoreboardId: "<",
                viewId: "<",
                currentSelection: '&',
                showBox: '<'
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
                            }
                        });
                    
                };

                $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
                    for (let i = 0; i < vm.view.Style.length; i++) {
                        let cssString = "";
                        angular.forEach(vm.view.Style[i], function (value, key) {
                            if (key != "StyleID" && key != "DivID" && value != null) {
                                let cssKey = '';
                                let splitAtCaps = key.match(/([A-Z]?[^A-Z]*)/g).slice(0, -1);
                                //console.log(splitAtCaps);
                                if (splitAtCaps.length == 1) {
                                    cssKey = key;
                                }
                                else {
                                    cssKey = splitAtCaps[0];
                                    for (let i = 1; i < splitAtCaps.length; i++) {
                                        cssKey += "-" + splitAtCaps[i];
                                    }
                                }
                                //console.log(cssKey);
                                cssString += cssKey + ":" + value + ";";
                            }
                        });
                        if (vm.showBox) {
                            cssString += "outline:1px dashed black;";
                        }
                        console.log(cssString);
                        document.getElementById(vm.view.Style[i].DivID).style.cssText = cssString;
                    }
                });
            }],
            templateUrl: "app/scoreboard/scoreboard-view.template.html"
        });
}());