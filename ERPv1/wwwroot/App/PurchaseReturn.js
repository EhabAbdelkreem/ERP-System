
PuchaseReturnVM = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);

    self.Save = function () {
        var data = ko.toJSON(self);
        $.ajax({
            url: '/Expenditure/Supplier/ReturnPurchase',
            type: "POST",
            data: ko.toJSON(self),
            contentType: "application/json",
            success: function (data) {
                if (data.newLocation !== null && data.newLocation !== undefined) {
                    window.location = data.newLocation;

                }
                if (data.errors !== null && data.errors !== undefined) {

                    self.Messages(data.errors);
                }

            }
        });
    };
  
    
}


