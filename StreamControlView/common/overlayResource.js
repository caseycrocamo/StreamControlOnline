(function(){
    angular.module("main")
        .factory("overlayResource",
                    ["$resource", "appSettings", overlayResource]);

    function overlayResource($resource, appSettings) {
        return $resource(
            appSettings.serverPath + "api/overlays/:id",
            null,
            {
                "getAll": { method: "GET", url: appSettings.serverPath + "api/overlays", isArray: true },
                "create": { method: "POST", url: appSettings.serverPath + "api/overlays" },
                "update": { method: "PUT" },
                "updateField": { method: "PUT", url: appSettings.serverPath + "api/overlays/Field" },
                "updatePlayer": { method: "PUT", url: appSettings.serverPath + "api/overlays/Player" },
                "updateStyle": { method: "PUT", url: appSettings.serverPath + "api/overlays/Style" }
            });
    }
}());