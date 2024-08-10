ChecksInBank = function (data) {
    var self = this;
    ko.mapping.fromJS(data, [], self);
    
    self.Save = function () {
      
        $.ajax({
            url: "/SalesArea/NR/CollectChecks",
            type: "POST",
            data: ko.toJSON(self),
            contentType: "application/json",
            success: function (data) {
                if (data.newLocation !== null || data.newLocation !== undefined) {
                    window.location = data.newLocation;
                }

                if (data.errors !== null) {
                
                }
            }
        });

    };
    self.hideMessages = function () {

        self.IsMessageAreaVisible(false);
    };
};