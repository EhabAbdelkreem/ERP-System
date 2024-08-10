var Mapping = {
    'TransactionDetails': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.JornalDetailId);
        },
        create: function (options) {
            return new JournalDetails(options.data);
        }
    }
};

JournalDetails = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    self.ChangeDebit = function () {
        self.Side(0);
    };
    self.ChangeCredit = function () {
        self.Side(1);
    };

    self.ChangeAccount = function () {
        var data = self.AccNum();
        $.ajax({
            url: '/GLArea/OpenBalance/GetAccountDetails/',
            type: "POST",
            data: ko.toJSON(self.AccNum()),
            contentType: "application/json",
            success: function (data) {
                if (data.Account !== null && data.Account !== undefined) {
                    self.UsedRate(data.Account.Currency.Rate);
                    self.CurrecnyAbbr(data.Account.Currency.CurrencyAbbrev);
                    self.CurrencyId(data.Account.Currency.Id);
                }     
            }
        });
    }

};


Journal = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);


    self.TotalDebit = ko.computed(function () {
        var total = 0;
        for (var i = 0, len = self.TransactionDetails().length; i < len; ++i) {
            if (self.TransactionDetails()[i].Side() === 0) {
                total = total + ((parseFloat(self.TransactionDetails()[i].Debit()) * parseFloat(self.TransactionDetails()[i].UsedRate())));
               
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
    self.AddAccount = function () {
        var Acc = new JournalDetails({
            JornalDetailId: 0,
            AccNum: '',
            CurrecnyAbbr: '',
            UsedRate:0,
            Side: '',
            Debit: 0,
            Credit: 0,
            CurrencyId:0
        });
        self.TransactionDetails.push(Acc);
    };

    self.RemoveAcc = function (AccDetails) {
        self.TransactionDetails.remove(AccDetails);
    };

    self.Save = function () {
        var data = ko.toJSON(self);
        $.ajax({
            url: '/GLArea/OpenBalance/SaveJournal/',
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
};