$(document).ready(function () {
    $.getJSON("/Home/GetData1", function (data) {
        var Names = []
        var Qts = []
        for (var i = 0; i < data.length; i++) {
            Names.push(data[i].date);
            Qts.push(data[i].count);
        }

        Highcharts.chart('container1', {
            chart: {
                type: 'line'
            },
            title: {
                text: 'Bilangan Kemasukan Pelajar Terkini '
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: Names
            },
            yAxis: {
                title: {
                    text: 'Jumlah Pelajar'
                }
            },
            plotOptions: {
                line: {
                    dataLabels: {
                        enabled: true
                    },
                    enableMouseTracking: true
                }
            },
            series: [{
                name: 'Pelajar',
                data: Qts
            }]
        });
    });
});
