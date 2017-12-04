(function () {
    angular.module("main").component("overlayView",
        {
            bindings: {
                OverlayID: "<",
                viewId: "<",
                currentSelection: '&',
                showBox: '<'
            },
            controllerAs: "vm",
            controller: ["overlayResource","authService", "$scope", function (overlayResource, authService, $scope) {
                var vm = this;
                vm.overlay = null;
                vm.message = "loading...";
                vm.$onInit = function () {
                    overlayResource.get({ id: vm.OverlayID })
                        .$promise
                        .then(function (data) {
                            vm.message = "";
                            vm.overlay = data;
                            for (let i = 0; i < vm.overlay.Views.length; i++) {
                                if (vm.overlay.Views[i].ViewID == vm.viewId) {
                                    vm.view = vm.overlay.Views[i];
                                }
                            };
                        });
                    
                };

                $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
                    for (let i = 0; i < vm.view.Style.length; i++) {
                        let cssString = "";
                        angular.forEach(vm.view.Style[i], function (value, key) {
                            if (key != "StyleID" && key != "DivID" && value != null) {
                                cssString += key + ":" + value + ";";
                            }
                            if (vm.showBox) {
                                cssString += "outline:1px dashed black;";
                            }
                        });
                        console.log(cssString);
                        document.getElementById(vm.view.Style[i].DivID).style.cssText = cssString;
                    }
                });
            }],
            templateUrl: "app/overlay/overlay-view.template.html"
        });
}());