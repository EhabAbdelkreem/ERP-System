ExpenseVM = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);




    self.SelectCurrency = function () {
       
        let Safe = $('#PaymentDetails_SafeAccNum');
        let Bank = $('#PaymentDetails_BankAccNum');
        let safeUrl = '/List/GetSafeAccounts';
        let bankUrl = '/List/GetBankAccounts';

       
        $.getJSON(safeUrl, { Id: self.ExpenseDetails.CurrencyId() }, function (response) {
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
            self.PaymentDetails.IsCustody(false);
        }
        if (payment === "30") {
            self.PaymentDetails.IsSafe(false);
            self.PaymentDetails.IsBank(true);
            self.PaymentDetails.IsCheck(false);
            self.PaymentDetails.IsCustody(false);
        }
        if (payment === "40") {
            self.PaymentDetails.IsSafe(false);
            self.PaymentDetails.IsBank(true);
            self.PaymentDetails.IsCheck(true);
            self.PaymentDetails.IsCustody(false);
        }

        if (payment === "50") {
            self.PaymentDetails.IsSafe(false);
            self.PaymentDetails.IsBank(false);
            self.PaymentDetails.IsCheck(false);
            self.PaymentDetails.IsCustody(true);
        }
    }


    self.Save = function () {
        var data = ko.toJSON(self);
        $.ajax({
            url: self.SaveURL(),
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
