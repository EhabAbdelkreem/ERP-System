SupplierPayment = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);




    self.SelectBalance = function (balance) {
        self.SelectedBalance.CurrencyId(balance.CurrencyId());
        self.SelectedBalance.Amount(balance.Amount());
        self.SelectedBalance.LocalAmount(balance.LocalAmount());
        self.SelectedBalance.Rate(balance.Rate());
        self.SelectedBalance.CurrencyAbbr(balance.CurrencyAbbr());

        let Safe = $('#PaymentDetails_SafeAccNum');
        let Bank = $('#PaymentDetails_BankAccNum');
        let safeUrl = '/List/GetSafeAccounts';
        let bankUrl = '/List/GetBankAccounts';

       
        $.getJSON(safeUrl, { Id: balance.CurrencyId() }, function (response) {
            Safe.empty(); 
            $("<option />", {
                val: "",
                text: "أختر ---"
            }).appendTo(Safe);
            $.each(response, function (index, item) {
                $("<option />", {
                    val: item.Value,
                    text: item.Text
                }).appendTo(Safe);
            });
            
        });

        $.getJSON(bankUrl, { Id: balance.CurrencyId() }, function (response) {
            Bank.empty();
            $("<option />", {
                val: "",
                text: "أختر ---"
            }).appendTo(Bank);
            $.each(response, function (index, item) {
                $("<option />", {
                    val: item.Value,
                    text: item.Text
                }).appendTo(Bank);
            });

        });

    }

    self.ChangePaymentMethod = function () {
        let payment = self.PaymentDetails.PaymentMethod();

        if (payment === "20") {
            self.PaymentDetails.IsSafe(true);
            self.PaymentDetails.IsBank(false);
            self.PaymentDetails.IsCheck(false);
        }
        if (payment === "30") {
            self.PaymentDetails.IsSafe(false);
            self.PaymentDetails.IsBank(true);
            self.PaymentDetails.IsCheck(false);
        }
        if (payment === "40") {
            self.PaymentDetails.IsSafe(false);
            self.PaymentDetails.IsBank(true);
            self.PaymentDetails.IsCheck(true);
        }
    }


    self.Save = function () {
        var data = ko.toJSON(self);
        $.ajax({
            url: '/Expenditure/Supplier/SaveSupplierPayment/',
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
