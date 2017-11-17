(function(){
    angular.module("main")
        .factory("scoreboardResource",
                    ["$resource", "appSettings", scoreboardResource]);

    function scoreboardResource($resource, appSettings) {
        return $resource(
            appSettings.serverPath + "api/Scoreboards/:id",
            null,
            {
                "getAll": { method: "GET", url: appSettings.serverPath + "api/Scoreboards", isArray: true },
                "create": { method: "POST", url: appSettings.serverPath + "api/Scoreboards" },
                "update": { method: "PUT" },
                "updateField": { method: "PUT", url: appSettings.serverPath + "api/Scoreboards/Field" },
                "updatePlayer": { method: "PUT", url: appSettings.serverPath + "api/Scoreboards/Player" },
                "updateStyle": { method: "PUT", url: appSettings.serverPath + "api/Scoreboards/Style" }
            });
    }
}());