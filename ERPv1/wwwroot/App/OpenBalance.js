OpeningBalance = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);


    self.TotalDebit = ko.computed(function () {
        var total = 0;
        for (var i = 0, len = self.TransactionDetails().length; i < len; ++i) {
            if (self.TransactionDetails()[i].Side() === 0) {
                total = total + ((parseFloat(self.TransactionDetails()[i].Debit()) * parseFloat(self.TransactionDetails()[i].UsedRate())));
                //total = total + parseFloat(self.TransactionDetails()[i]).Debit()
            }
        }

        return total;
    });

    self.TotalCredit = ko.computed(function () {
        var total = 0;
        for (var i = 0, len = self.TransactionDetails().length; i < len; ++i) {
            if (self.TransactionDetails()[i].Side() === 1) {
                total = total + ((parseFloat(self.TransactionDetails()[i].Credit()) * parseFloat(self.TransactionDetails()[i].UsedRate())));
            }
        }

        return total;
    });

    self.Save = function () {
        var data = ko.toJSON(self);
        $.ajax({
            url: '/GLArea/OpenBalance/SaveOpeningBalance/',
            type: "POST",
            data: ko.toJSON(self),
            contentType: "application/json",
            success: function (data) {
                if (data.newLocation !== null) {
                    window.location = data.newLocation;

                }
                if (data.errors !== null) {

                    self.Messages(data.errors);
                }

            }
        });
    };
};