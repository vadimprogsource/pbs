function initDatePicker(startDate)
{
    if(!startDate) {
        startDate = new Date();
    }
    var selectedDates = [];
    var preventUpdate = false;
    var serverDateTimeFormat = "yyyy-mm-ddTHH:MM:SSTZHM";

    var qq, dates = $('#bySelectedDateValue').val();
    dates = dates ? dates.split(/,/) : [];
    for(qq = 0; qq < dates.length; ++qq) {
        var dd = new Date(dates[qq]);
        dd.setHours(0,0,0,0);
        selectedDates.push(dd);
    }


    $('#bySelected').each(function()
    {
        var picker = $(this).find('.bySelectedDateDatepicker');
        var val = $(this).find('#bySelectedDateValue');
        var curDate = new Date(startDate);
        curDate.setHours(0,0,0,0);

        picker.each(function(/*index*/)
        {
            var $curDatePicker = $(this);
            var viewDates = new Date(curDate.getFullYear(), curDate.getMonth() + 1, 0);
            // if(curDate.getMonth() == 11) {
            //     viewDates = new Date(curDate.getFullYear() + 1, 0, 1);
            // } else {
            //     viewDates = new Date(curDate.getFullYear(), curDate.getMonth() + 1, 0);
            // }
            // viewDates = new Date(viewDates.getTime() - (1000 * 60 * 60 * 24));
            $curDatePicker.datepicker({
                multidate: true,
                startDate: curDate,
                language: 'ru',
                endDate: viewDates //( index != (picker.length -1) ) ? viewDates : false
            });
            curDate = new Date(viewDates.getTime() + (1000 * 60 * 60 * 24));
            $curDatePicker.on("changeDate", function()
            {
                if(!preventUpdate) updateSelectedDays(picker, val);
            });
        });
    });

    updatePickerViewRanges();

    function updateSelectedDays(picker, val)
    {
        var viewStartDate = new Date(startDate);
        var viewEndDate = new Date(viewStartDate.getFullYear(), viewStartDate.getMonth() + 4, 0, 25, 59, 59, 999);
        var value = [];
        val.val('');
        picker.each(function()
        {
            var dates = $(this).datepicker('getDates');
            if(dates && dates.length > 0) {
                value = value.concat(dates);
            }
        });

        // remove dates outside visible region
        // and attach new selected dates
        selectedDates = selectedDates.filter(function(v)
        {
            return (v.getTime() < viewStartDate.getTime() || v.getTime() > viewEndDate.getTime());
        });
        selectedDates = selectedDates.concat(value);

        val.val(selectedDates.map(function(i)
        {
            return i.formatTo(serverDateTimeFormat);
        }).join());
    }

    // it's worst solution, but in case how datepickers initializing now -- is time-saving solution
    window.initDatePicker_setSelectedDates = function(dates) {
        selectedDates = dates;
        var val = $('#bySelectedDateValue');
        val.val(selectedDates.map(function(i)
        {
            return i.formatTo(serverDateTimeFormat);
        }).join());
        updatePickerViewRanges();
    };

    function updatePickerViewRanges()
    {
        var curDate = new Date(startDate);
        curDate.setHours(0,0,0,0);
        var picker = $("#bySelected").find('.bySelectedDateDatepicker');

        preventUpdate = true;
        picker.each(function(index)
        {
            var $curDatePicker = $(this);
            var viewDates = new Date(curDate.getFullYear(), curDate.getMonth() + 1, 0);

            // it's more easier and betters solution
            // becouse datepicker lives to switch view to current month
            $curDatePicker.datepicker("remove");
            $curDatePicker.datepicker({
                multidate: true,
                startDate: curDate,
                language: 'ru',
                endDate: viewDates
            });

            // restore selected dates
            var sel = [];
            selectedDates.forEach(function(v)
            {
                if(v.getTime() >= curDate.getTime() && curDate.getTime() <= viewDates.getTime()) {
                    sel.push(new Date(v));
                }
            });
            if(sel.length > 0) {
                $curDatePicker.datepicker("setDates", sel);
            }
            curDate = new Date(viewDates.getTime() + (1000 * 60 * 60 * 24));
        });
        preventUpdate = false;
    }

    function toPrev()
    {
        startDate = new Date(startDate.getFullYear(), startDate.getMonth() - 4, 1);
        if(startDate.getTime() <= Date.now()) {
            startDate = new Date();
            $("#window-popup-modal").find(".btn.move-prev").css("visibility", "hidden");
        } else {
            $("#window-popup-modal").find(".btn.move-prev").css("visibility", "visible");
        }
        updatePickerViewRanges();
    }

    function toNext()
    {
        startDate = new Date(startDate.getFullYear(), startDate.getMonth() + 4, 1);
        updatePickerViewRanges();
        $("#window-popup-modal").find(".btn.move-prev").css("visibility", "visible");
    }

    $("#window-popup-modal").find(".btn.move-prev").css("visibility", "hidden").on("click", toPrev);
    $("#window-popup-modal").find(".btn.move-next").css("visibility", "visible").on("click", toNext);
}


$(initDatePicker);