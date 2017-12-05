(function () {
    angular.module("main").component("overlayEdit", {
        bindings: {
            overlayId: "<"
        },
        controllerAs: "vm",
        controller: ["overlayResource", "$location", "$timeout", function (overlayResource, $location, $timeout) {
            var vm = this;
            vm.overlay = {};
            vm.message = 'loading...';

            vm.$onInit = function () {
                overlayResource.get({ id: vm.overlayId })
                    .$promise
                    .then(
                    //on success
                    function (data) {
                        vm.message = "";
                        vm.overlay = data;
                        vm.originaloverlay = angular.copy(data);
                    },
                    //on failure
                    function () {
                        vm.message = "this shit didnt load, are you logged in?"
                    });

                vm.cancel = function () {
                    vm.overlay = vm.originaloverlay;
                    vm.message = "Changes Reverted";
                };

                vm.returnToList = function () {
                    $location.path("/overlays");
                };

                vm.save = function () {
                    vm.message = "saving...";
                    overlayResource.update({ id: vm.overlayId }, vm.overlay).$promise.then(function () {
                        vm.message = "wow you saved!";
                        overlayResource.get({ id: vm.overlayId }).$promise.then(function (data) {
                            vm.overlay = data;
                            vm.originaloverlay = angular.copy(data);
                        });
                    });
                };
            }
            

            
        }],
        templateUrl: "app/overlay/overlay-edit.template.html"
    });
}());