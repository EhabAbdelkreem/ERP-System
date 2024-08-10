NPVM = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);

    self.SelectNote = function (np) {
        self.SelectedNote.ChkNum(np.ChkNum());
        self.SelectedNote.WritingDate(np.WritingDate());
        self.SelectedNote.DueDate(np.DueDate());
        self.SelectedNote.AmountLocal(np.AmountLocal());
        self.SelectedNote.AmountForgin(np.AmountForgin());
        self.SelectedNote.CurrencyId(np.CurrencyId());
        self.SelectedNote.CurrencyAbbrev(np.CurrencyAbbrev());
        self.SelectedNote.BankAccountNum(np.BankAccountNum());
        self.SelectedNote.BankAccountName(np.BankAccountName());
        self.SelectedNote.SupplierId(np.SupplierId());
        self.SelectedNote.SupplierName(np.SupplierName());
        self.SelectedNote.Paid(np.Paid());


        let Safe = $('#PaymentDetails_SafeAccNum');
        let Bank = $('#PaymentDetails_BankAccNum');
        let safeUrl = '/List/GetSafeAccounts';
        let bankUrl = '/List/GetBankAccounts';


        $.getJSON(safeUrl, { Id: np.CurrencyId }, function (response) {
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

        $.getJSON(bankUrl, { Id: np.CurrencyId }, function (response) {
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

    self.Collect = function (np) {
        var data = ko.toJSON(np);

        $.ajax({
            url: '/Expenditure/NP/CollectCheck/',
            type: "POST",
            data: data,
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
    }

    self.MoveToCash = function (np) {
        var data = ko.toJSON(np);

        $.ajax({
            url: '/Expenditure/NP/MoveToCashCollection/',
            type: "POST",
            data: data,
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
            url: '/Expenditure/NP/CollectCashCollection/',
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