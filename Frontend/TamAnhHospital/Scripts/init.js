$(function () {
    $('[data-toggle="tooltip"]').tooltip();

    var $scrollable = $(".scrollable"),
        $scrollbar = $(".scrollbar"),
        height = $scrollable.outerHeight(true),    // visible height
        scrollHeight = $scrollable.prop("scrollHeight"), // total height
        barHeight = height * height / scrollHeight;   // Scrollbar height!
    // Scrollbar drag:
    $scrollbar.height(barHeight).draggable({
        axis: "y",
        containment: "parent-scroll",
        drag: function (e, ui) {
            $scrollable.scrollTop(scrollHeight / height * ui.position.top);
        }
    });
    // Element scroll:
    $scrollable.scroll(function () {
        $scrollbar.css({ top: $scrollable.scrollTop() / height * barHeight });
        console.log($scrollable.scrollTop() / height * barHeight);
    });
})
$.fn.clearData = function ($form) {
    $form.find('input:text, input:password, input:file, select, textarea').val('');
    $form.find('input:radio, input:checkbox')
        .removeAttr('checked').removeAttr('selected');
}
$.fn.callDataTable = function (lang, disableColumn) {
    var array = [];
    $.each(disableColumn.split(','), function (idx, val) {
        array.push(parseInt(val));
    });

    var setVal = 'en';
    if (lang == 'vi') setVal = 'Vietnamese';
    if (disableColumn == '') { disableColumn = 0; }

    $(this).DataTable({
        "bPaginate": true,
        "pageLength": 30,
        "responsive": false,
        "searching": true,
        "info": false,
        "bLengthChange": false,
        "language": { "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/" + setVal + ".json" },
        "order": [],
        "columnDefs": [{ orderable: false, targets: array }]
    });
}

$.fn.callDataTableCustomSearch = function (lang, disableColumn, controlSearch) {
    var array = [];
    $.each(disableColumn.split(','), function (idx, val) {
        array.push(parseInt(val));
    });

    var setVal = 'en';
    if (lang == 'vi') setVal = 'Vietnamese';
    if (disableColumn == '') { disableColumn = 0; }

    var table = $(this).DataTable({
        "bPaginate": true,
        "pageLength": 30,
        "responsive": false,
        "searching": true,
        "info": false,
        "bLengthChange": false,
        "language": { "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/" + setVal + ".json" },
        "order": [],
        "columnDefs": [{ orderable: false, targets: array }]
    });

    $(controlSearch).on('keyup', function () {
        table.search(this.value).draw();
    });
}
$.fn.callDataTableCustomSearchPage = function (lang, disableColumn, controlSearch, count) {
    var array = [];
    $.each(disableColumn.split(','), function (idx, val) {
        array.push(parseInt(val));
    });

    var setVal = 'en';
    if (lang == 'vi') setVal = 'Vietnamese';
    if (count == 0 || count == '') count = 30;
    if (disableColumn == '') { disableColumn = 0; }

    var table = $(this).DataTable({
        "bPaginate": true,
        "pageLength": count,
        "responsive": false,
        "searching": true,
        "info": false,
        "bLengthChange": false,
        "language": { "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/" + setVal + ".json" },
        "order": [],
        "columnDefs": [{ orderable: false, targets: array }]
    });

    $(controlSearch).on('keyup', function () {
        table.search(this.value).draw();
    });
}
$.fn.callModal = function (url) {
    $("body").find(".modal-backdrop").remove();
    $.ajax({
        url: url,
        dataType: 'html',
        success: function (data) {
            $('#partialContainer').html(data);
            $("#staticModal").modal({ backdrop: 'static', keyboard: false });
        }, error: function (xhr, status) {
            switch (status) {
                case 404:
                    $(this).callToast("error", 'Thông báo', 'File not found');
                    break;
                case 500:
                    $(this).callToast("error", 'Thông báo', 'Server error');
                    break;
                case 0:
                    $(this).callToast("error", 'Thông báo', 'Request aborted');
                    break;
                default:
                    $(this).callToast("error", 'Thông báo', 'Unknown error ' + status);
            } 
        }
    });
}

$.fn.setSelectValue = function (control, value) {
    if (value == "Khác") {
        $(control).val("");
    } else {
        $(control).val(value);
    }
}

$.fn.callToast = function (status, head, message) {
    if (status == "success") {
        $.toast({
            heading: head,
            text: message,
            showHideTransition: 'fade',
            icon: 'success',
            position: 'top-center',
            hideAfter: 5000,
            afterHidden: function () {
                $(".jq-toast-wrap").remove();
            },
            stack: false
        })
    } else if (status == "warning") {
        $.toast({
            heading: head,
            text: message,
            showHideTransition: 'fade',
            icon: 'warning',
            position: 'top-center',
            hideAfter: 5000,
            afterHidden: function () {
                $(".jq-toast-wrap").remove();
            }, stack: false
        })
    } else {
        $.toast({
            heading: head,
            text: message,
            showHideTransition: 'fade',
            icon: 'error',
            position: 'top-center',
            hideAfter: 5000,
            afterHidden: function () {
                $(".jq-toast-wrap").remove();
            }, stack: false
        })
    }
}
function Calculator_Room() {
    //Tính toán số lượng trong buồng
    var counter_empty = 0;
    var counter_warning = 0;
    var counter_expired = 0;
    var counter_total = 0;

    $(".counter_room_empty").each(function (idx, value) {
        counter_empty += parseInt($(this).html());
    });
    $(".counter_room_warning").each(function () {
        counter_warning += parseInt($(this).html());
    });
    $(".counter_room_expired").each(function () {
        counter_expired += parseInt($(this).html());
    });
    $(".counter_room_total").each(function () {
        counter_total += parseInt($(this).html());
    });

    $("#total_room_empty").html(counter_empty);
    $("#total_room_warning").html(counter_warning);
    $("#total_room_expired").html(counter_expired);
    $("#total_room_total").html(counter_total);
};

function callChoosen() {
    var config = {
        '.chosen-select': {},
        '.chosen-select-deselect': { allow_single_deselect: true },
        '.chosen-select-no-single': { disable_search_threshold: 10 },
        '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
        '.chosen-select-width': {},
    }
    for (var selector in config) {
        $(selector).chosen(config[selector]);
    }
}
function call_ajax_table(data, table_id, columns) {
    table_id = "#" + table_id;

    $.fn.dataTable.moment('DD/MM/YYYY');

    var _table = $(table_id).DataTable({
        "ajax": {
            "url": data,
            "type": "GET",
            "datatype": "json",
        },
        "pageLength": 20,
        "autoWidth": true,
        "processing": true,
        "columns": columns,
        "deferRender": true,
        "columnDefs": [{
            "targets": 'no-sort',
            "orderable": false
        }],
        "language": {
            "sProcessing": "Đang xử lý...",
            "sLengthMenu": "Xem _MENU_ mục",
            "sZeroRecords": "Không tìm thấy dòng nào phù hợp",
            "sInfo": "Đang xem _START_ đến _END_ trong tổng số _TOTAL_ mục",
            "sInfoEmpty": "Đang xem 0 đến 0 trong tổng số 0 mục",
            "sInfoFiltered": "(được lọc từ _MAX_ mục)",
            "sInfoPostFix": "",
            "sSearch": "Tìm:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "Đầu",
                "sPrevious": "Trước",
                "sNext": "Tiếp",
                "sLast": "Cuối"
            }
        }
    });
    _table.on('page.dt order.dt draw.dt', function () {
        _table.column(0, { search: 'applied', order: 'applied', page: 'current' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1 + _table.page.info().start;
        });
    }).draw();

    $(table_id).on('processing.dt', function (e, settings, processing) {
        $('#processingIndicator').css('display', processing ? 'block' : 'none');
    }).dataTable();
}