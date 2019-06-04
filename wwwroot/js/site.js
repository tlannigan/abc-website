$(document).ready(function () {

    // Change uploaded file name on Create views
    $('input[type="file"').change(function (e) {
        var fileName = e.target.files[0].name;
        document.getElementById("fileLabel").innerHTML = fileName;
    });

    // WebFontLoader for Google Fonts
    WebFontConfig = {
        google: { families: ['Orbitron', 'Raleway:700'] }
    };
    (function () {
        var wf = document.createElement('script');
        wf.src = ('https:' == document.location.protocol ? 'https' : 'http') +
            '://ajax.googleapis.com/ajax/libs/webfont/1.6.26/webfont.js';
        wf.type = 'text/javascript';
        wf.async = 'true';
        var s = document.getElementsByTagName('script')[0];
        s.parentNode.insertBefore(wf, s);
    })();
    
    // jQuery Data Table Init
    let dataTable;
    if ($("#product-table").length) {
        dataTable = $("#product-table").DataTable({
            responsive: {
                details: { type: "column", target: "tr" }
            },
            columnDefs: [
                { targets: priorityTargets, responsivePriority: 0 },
                { targets: controlTarget, className: 'control', orderable: false },
                { targets: truncTargets, className: "truncate" },
                { targets: [-1], searchable: false }
            ],
            createdRow: function (row) {
                var td = $(row).find(".truncate");
                td.attr("title", td.html());
            },
            "dom": '<"top"i>rt<"bottom"><"clear">',
            "paging": false,
            "bInfo": false
        });

        $("#tableSearch").keyup(function () {
            dataTable.search($(this).val()).draw();
        });
    };

    // Customer Chat Bubble
    Tawk_API = Tawk_API || {};
    Tawk_API.onLoad = function () {
        $(".openQuote").on("click", function () {
            Tawk_API.maximize();
        });
    };

    // Rerender Facebook Page Plugin on window size change
    $(window).resize(function () {
        $('#fb-feed').html('<div class="fb-page" data-href="https://www.facebook.com/ABC-Muffler-and-Hitch-165717576773960/" data-tabs="timeline" data-width="" data-height="600" data-small-header="true" data-adapt-container-width="true" data-hide-cover="false" data-show-facepile="true"><blockquote cite="https://www.facebook.com/ABC-Muffler-and-Hitch-165717576773960/" class="fb-xfbml-parse-ignore"><a href="https://www.facebook.com/ABC-Muffler-and-Hitch-165717576773960/">ABC Muffler and Hitch</a></blockquote></div>');
        FB.XFBML.parse();
        dataTable.columns.adjust();
    });

});
