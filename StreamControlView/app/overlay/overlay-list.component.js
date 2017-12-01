(function () {
    angular.module("main").component("overlayList",
        {
            bindings: {
                username: "<"
            },
            controllerAs: "vm",
            controller: ["overlayResource", function (overlayResource) {
                var vm = this;
                vm.message = "";
                
                vm.$onInit = function () {
                    vm.overlays = {};
                    overlayResource.getAll({ username: vm.username })
                        .$promise.then(function (data) {
                            vm.overlays = data;
                        });
                }; 
                vm.remove = function (_id) {
                    overlayResource.remove({ id: _id })
                        .$promise.then(function () {
                            overlayResource.getAll({ username: vm.username })
                                .$promise.then(function (data) {
                                    vm.overlays = data;
                                });
                        });
                };
            }],
            templateUrl: "app/overlay/overlay-list.template.html"
        });
}());