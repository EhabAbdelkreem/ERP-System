
Statment = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);


   
    self.GetStatment = function () {
        var data = ko.toJSON(self.StatmentParams);
        $.ajax({
            url: self.ReportURL(),
            type: "POST",
            data: data,
            contentType: "application/json",
            success: function (data) {
                self.Transactions.removeAll();
                ko.mapping.fromJS(data.result, {}, self);
            }
        });
    };
};