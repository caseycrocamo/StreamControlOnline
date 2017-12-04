(function () {
    angular.module("main").component("viewEdit",
        {
            bindings: {
                OverlayID: "<",
                viewId: "<"
            },
            controllerAs: "vm",
            controller: ["overlayResource", "authService", "$scope", "$filter","$location", function (overlayResource, authService, $scope, $filter, $location) {
                var vm = this;
                vm.overlay = null;
                vm.message = "loading...";
                vm.currentId = "";
                vm.currentStyle = {};
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
                        })

                    $scope.$watch('vm.currentId', currentIdChanged);
                };

                vm.currentSelection = function (event) {
                    var id = event.srcElement.id;
                    console.log(id);
                    vm.currentId = id;
                };

                vm.cancel = function () {
                    vm.currentStyle = vm.originalStyle;
                    vm.message = "Changes Reverted";
                };

                vm.returnToList = function () {
                    $location.path("/overlays");
                };

                vm.save = function () {
                    vm.message = "saving...";
                    overlayResource.updateStyle({ id: vm.currentStyle.StyleID }, vm.currentStyle).$promise.then(function () {
                        vm.message = "wow you saved!";
                        overlayResource.get({ id: vm.OverlayID }).$promise.then(function (data) {
                            vm.overlay = data;
                            vm.originaloverlay = angular.copy(data);
                        });
                    });
                };

                function currentIdChanged() {
                    if (vm.view !== undefined) {
                        let styleObj = $filter('getById')(vm.view.Style, vm.currentId);
                        vm.originalStyle = styleObj;
                        vm.currentStyle = styleObj;
                    };
                };

                
            }],
            templateUrl: "app/overlay/view/view-edit.template.html"
        });
}());