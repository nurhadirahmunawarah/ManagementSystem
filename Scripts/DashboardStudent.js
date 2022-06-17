// Build the chart

$(document).ready(function () {
    $.getJSON("/Home/GetData3", function (data) {


        Highcharts.chart('container4', {
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false,
                type: 'pie'
            },
            title: {
                text: 'Kadar Penilaian daripada Tutor'
            },
            tooltip: {
                pointFormat: 'Skor {series.name}: <b> {point.y}</b>'
            },
            accessibility: {
                point: {
                    valueSuffix: '%'
                }
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: false
                    },
                    showInLegend: true
                }
            },
            series: [{
                name: 'Bilangan Penilaian',
                colorByPoint: true,
                data: [{
                    name: '1',
                    y: data.rating1,

                }, {
                    name: '2',
                    y: data.rating1
                }, {
                    name: '3',
                    y: data.rating1
                }
                    , {
                    name: '4',
                    y: data.rating4
                }
                    , {
                    name: '5',
                    y: data.rating5
                }





                ]
            }]
        });




    });

});









