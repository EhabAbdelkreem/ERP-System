TrailBalanceKOViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);
   
    self.Search = function () {
       
        $.ajax({
            url: self.ReportURL(),
            type: "POST",
            data: ko.toJSON(self),
            contentType: "application/json",
            success: function (data) {
                if (data.result !== null) {
                    
                    ko.mapping.fromJS(data.result, {}, self);
                }

            }
        });

    };
    
};