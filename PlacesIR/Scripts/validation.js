//#region ======================================== Validation =====================================================>

ValidationRule = function () {
    this.Input = null;
    this.ValidationContainer = null;
    this.ErrorContainer = false;
    this.Required = false;
    this.RequiredMessage = '';
    this.Regex = false;
    this.ErrorMessage = '';
    this.CustomeFunction = false;
    this.WaterMarkText = false;
    this.IsValid = false;
    this.ValidMask = '&nbsp;';
    this.ErrorMask = '{0}';
    var ThisRule = this;

    this._Trim = function (stringToTrim) {
        return stringToTrim.replace(/^\s+|\s+$/g, "");
    };

    this.Validate = function (CallValidateCallBack, CallValidateItemCallBack) {
        if (typeof CallValidateCallBack == "undefined") {
            CallValidateCallBack = true;
        }
        var value = ThisRule.Input.val();
        if (ThisRule._Trim(value) == '') {
            if (ThisRule.Required) {
                ThisRule.IsValid = false;
                if (CallValidateCallBack) {
                    ThisRule.ValidateCallBack(ThisRule.RequiredMessage, CallValidateItemCallBack);
                }
                return false;
            }
            else {
                ThisRule.IsValid = true;
                if (ThisRule.ErrorContainer) {
                    ThisRule.ErrorContainer.html('&nbsp;');
                }
                if (CallValidateItemCallBack && ThisRule.AdditionalValidateCallBack) {
                    ThisRule.AdditionalValidateCallBack(ThisRule);
                }
                return true;
            }
        }
        if (ThisRule.Required && ThisRule.WaterMarkText && value == ThisRule.WaterMarkText) {
            ThisRule.IsValid = false;
            if (CallValidateCallBack) {
                ThisRule.ValidateCallBack(ThisRule.RequiredMessage, CallValidateItemCallBack);
            }
            return false;
        }
        if ((!ThisRule.Required && ThisRule.WaterMarkText && value == ThisRule.WaterMarkText) ||
			(!ThisRule.Required && !ThisRule.WaterMarkText && ThisRule._Trim(value) == '')) {
            ThisRule.IsValid = true;
            if (ThisRule.ErrorContainer) {
                ThisRule.ErrorContainer.html('');
            }
            if (CallValidateItemCallBack && ThisRule.AdditionalValidateCallBack) {
                ThisRule.AdditionalValidateCallBack(ThisRule);
            }
            return true;
        }
        if (ThisRule.Regex) {
            var reg = new RegExp(ThisRule.Regex, 'gi');
            if (!reg.test(value)) {
                ThisRule.IsValid = false;
                if (CallValidateCallBack) {
                    ThisRule.ValidateCallBack(ThisRule.ErrorMessage, CallValidateItemCallBack);
                }
                return false;
            }
        }
        if (ThisRule.CustomeFunction && typeof window[ThisRule.CustomeFunction] === "function") {
            if (!window[ThisRule.CustomeFunction](value)) {
                ThisRule.IsValid = false;
                if (CallValidateCallBack) {
                    ThisRule.ValidateCallBack(ThisRule.ErrorMessage, CallValidateItemCallBack);
                }
                return false;
            }
        }
        ThisRule.IsValid = true;
        if (CallValidateCallBack) {
            ThisRule.ValidateCallBack('', CallValidateItemCallBack);
        }
        return true;
    };
    this.ValidateCallBack = function (message, CallValidateItemCallBack) {
        if (ThisRule.ErrorContainer) {
            if (ThisRule.IsValid) {
                ThisRule.ErrorContainer.html(ThisRule.ValidMask);
                ThisRule.ValidationContainer.removeClass('has-error');
            }
            else {
                ThisRule.ErrorContainer.html(ThisRule.ErrorMask.replace('{0}', message));
                ThisRule.ValidationContainer.addClass('has-error');
            }
        }
        if (CallValidateItemCallBack && ThisRule.AdditionalValidateCallBack) {
            ThisRule.AdditionalValidateCallBack(ThisRule);
        }
    }
    this.AdditionalValidateCallBack = false;
    this.BindOnBlurValidation = function () {
        ThisRule.Input.blur(function (e) { ThisRule.Validate(e, true); });
    };
};
(function ($) {
    $.fn.ValidationGroup = function (options) {
        var defaults = {
            OnInit: false,
            ValidationItemCallback: false
        };
        var settings = $.extend(defaults, options);
        var self = this;
        this.ValidationRules = new Array();

        this._init = function () {
            var ValidatioRulesContainer = $(self);
            fillAllfieldtext = ValidatioRulesContainer.data("fillallfieldtext") != null ? ValidatioRulesContainer.data("fillallfieldtext") : '';
            ValidatioRulesContainer.children().each(function () {
                var ruleObj = $(this);
                var newRule = new ValidationRule();
                newRule.Input = $("#" + ruleObj.data("controltovalidate"));
                newRule.ValidationContainer = newRule.Input.closest('.form-group');
                if (ruleObj.data("errorcontainerid") == null) {
                    newRule.ErrorContainer = newRule.ValidationContainer.find('.with-errors');
                }
                else {
                    newRule.ErrorContainer = $("#" + ruleObj.data("errorcontainerid"));
                }
                newRule.Required = ruleObj.data("required");
                newRule.RequiredMessage = ruleObj.data("requirederrormessage");
                newRule.ErrorMessage = ruleObj.data("errormessege");
                newRule.Regex = ruleObj.data("regex") != null ? ruleObj.data("regex").replace(/&quote;/g, "\"") : false;
                newRule.CustomeFunction = ruleObj.data("customefunction") != null ? ruleObj.data("customefunction") : false;
                newRule.WaterMarkText = ruleObj.data("watermark") != null ? ruleObj.data("watermark") : false;
                newRule.BindOnBlurValidation();
                if (settings.ValidationItemCallback) {
                    newRule.AdditionalValidateCallBack = function () { settings.ValidationItemCallback(newRule); };
                }
                self.ValidationRules.push(newRule);
            });
            if (settings.OnInit) {
                settings.OnInit();
            }
        }
        this.ValidateAll = function ValidateAll(CallValidateCallBack, CallValidateItemCallBack) {
            if (typeof CallValidateCallBack == "undefined") {
                CallValidateCallBack = true;
            }
            if (typeof CallValidateItemCallBack == "undefined") {
                CallValidateItemCallBack = false;
            }
            var isValid = true;
            for (var VRule in self.ValidationRules) {
                if (!isNaN(VRule)) {
                    isValid = isValid & self.ValidationRules[VRule].Validate(CallValidateCallBack, CallValidateItemCallBack);
                }
            }
            return isValid;
        }
        this._init();
        return this;
    }
})(jQuery);

function ValidateID(str) {
    // Just in case -> convert to string
    var IDnum = String(str);

    // Validate correct input
    if ((IDnum.length > 9) || (IDnum.length < 5)) {
        return false;
    }
    if (isNaN(IDnum)) {
        return false;
    }
    // The number is too short - add leading 0000
    if (IDnum.length < 9) {
        while (IDnum.length < 9) {
            IDnum = '0' + IDnum;
        }
    }
    // CHECK THE ID NUMBER
    var mone = 0, incNum;
    for (var i = 0; i < 9; i++) {
        incNum = Number(IDnum.charAt(i));
        incNum *= (i % 2) + 1;
        if (incNum > 9)
            incNum -= 9;
        mone += incNum;
    }
    if (mone % 10 == 0)
        return true;
    else
        return false;
}

//#endregion