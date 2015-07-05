//#region //=============================================== DateTime ================================================
if (typeof (Date.prototype.ClearTime) == 'undefined') {
    Date.prototype.ClearTime = function () {
        return new Date(this.getFullYear(), this.getMonth(), this.getDate());
    };
}
if (typeof (Array.prototype.remove) == 'undefined') {
    Array.prototype.remove = function (from, to) {
        var rest = this.slice((to || from) + 1 || this.length);
        this.length = from < 0 ? this.length + from : from;
        return this.push.apply(this, rest);
    };
}
if (!Array.prototype.forEach) {
    Array.prototype.forEach = function (fun /*, thisp*/) {
        var len = this.length;
        if (typeof fun != "function")
            throw new TypeError();
        for (var i = 0; i < len; i++) {
            if (i in this) {
                fun.call(this, this[i]);
            }
        }
    };
}
TryParseJsonFormToDate = function (value, isClear) {
    if (!value) return null;
    var dt = null;
    if (value instanceof Date) {
        dt = value;
    }
    else if (value.toString().indexOf('Date') >= 0 || value.toString().indexOf('\Date') >= 0) {
        a = /^\/Date\((-?[0-9]+)\)\/$/.exec(value);
        if (a) {
            if (isClear) {
                dt = new Date(parseInt(a[1], 10)).ClearTime();
            }
            else {
                dt = new Date(parseInt(a[1], 10));
            }
        }
    }
    else {
        dt = new Date(1, 1, 1970);
    }
    return dt;
}
if (typeof (Date.prototype.Format) == 'undefined') {
    Date.prototype.Format = function (f, useCulture) {
        if (!this.valueOf())
            return '';

        var gsMonthNames;
        var gsDayNames;
        if (!useCulture) {
            gsMonthNames = new Array(
				'January',
				'February',
				'March',
				'April',
				'May',
				'June',
				'July',
				'August',
				'September',
				'October',
				'November',
				'December'
			);

            gsDayNames = new Array(
				'Sunday',
				'Monday',
				'Tuesday',
				'Wednesday',
				'Thursday',
				'Friday',
				'Saturday'
			);
        }
        else {
            gsMonthNames = window.globalizedMMDatePickerMonthNames;
            gsDayNames = window.globalizedMMDatePickerDayNames;
        }
        var d = this;

        return f.replace(/(yyyy|yy|MMMM|MMM|MM|M|dddd|ddd|dd|d|HH|hh|mm|ss|fff|a\/p)/gi,
			function ($1) {
			    switch ($1) {
			        case 'yyyy': return d.getFullYear();
			        case 'yy': return d.getFullYear() % 100 < 10 ? '0' + d.getFullYear() % 100 : d.getFullYear() % 100;
			        case 'MMMM': return gsMonthNames[d.getMonth()];
			        case 'MMM': return gsMonthNames[d.getMonth()].substr(0, 3);
			        case 'MM': return (d.getMonth() + 1) < 10 ? '0' + (d.getMonth() + 1) : (d.getMonth() + 1);
			        case 'M': return d.getMonth() + 1;
			        case 'dddd': return gsDayNames[d.getDay()];
			        case 'ddd': return gsDayNames[d.getDay()].substr(0, 3);
			        case 'dd': return d.getDate() < 10 ? '0' + d.getDate() : d.getDate();
			        case 'd': return d.getDate();
			        case 'HH': return d.getHours() < 10 ? '0' + d.getHours() : d.getHours();
			        case 'hh': return ((h = d.getHours() % 12) ? h : 12) < 10 ? '0' + ((h = d.getHours() % 12) ? h : 12) : ((h = d.getHours() % 12) ? h : 12);
			        case 'mm': return d.getMinutes() < 10 ? '0' + d.getMinutes() : d.getMinutes();
			        case 'ss': return d.getSeconds() < 10 ? '0' + d.getSeconds() : d.getSeconds();
			        case 'fff': return d.getMilliseconds() < 100 ? ('0' + (d.getMilliseconds() < 10 ? '0' + d.getMilliseconds() : d.getMilliseconds())) : d.getMilliseconds();
			        case 'a/p': return d.getHours() < 12 ? 'am' : 'pm';
			    }
			}
		);
    };
}
//#endregion