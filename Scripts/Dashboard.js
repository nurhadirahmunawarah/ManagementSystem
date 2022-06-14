// Build the chart

$(document).ready(function () {
        $.getJSON("/Home/GetData", function (data) {


            Highcharts.chart('container', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: 'Bilangan Pengguna Sistem ini'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.y}</b>'
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
                    name: 'Bilangan',
                    colorByPoint: true,
                    data: [{
                        name: 'Admin',
                        y: data.Admin,
                       
                    }, {
                        name: 'Tutor',
                        y: data.Tutor
                    }, {
                        name: 'Pelajar',
                        y: data.Student
                    }]
                }]
            });




        });
  
});









