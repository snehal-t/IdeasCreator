function drawDashboard(dashboard) {
  Highcharts.chart('container', {
    chart: {
      plotBackgroundColor: null,
      plotBorderWidth: 0,
      plotShadow: false,
      backgroundColor: 'rgba(255, 255, 255, 0.0)'
    },
    title: {
      text: 'Total Ideas<br>' + dashboard.totalIdeas,
      style: {
        color: 'white'
      },
      align: 'center',
      verticalAlign: 'middle',
      y: 70
    },
    tooltip: {
      //pointFormat: '',
      color: 'blue',
      fontWeight: 'bold'

    },
    plotOptions: {
      pie: {
        dataLabels: {
          enabled: false,
          distance: -10,
          style: {
            fontWeight: 'bolder',
            color: 'green'
          }
        },
        startAngle: 180,
        endAngle: 90,
        center: ['50%', '75%'],
        size: '110%'
      }
    },
    series: [{
      type: 'pie',
      name: 'Idea status',
      innerSize: '70%',
      data: [
        ['Completed: ' + dashboard.ideasAccepted, dashboard.ideasAccepted],
        ['In Action: ' + dashboard.ideasInAction, dashboard.ideasInAction],
        ['Ready to pick: ' + dashboard.ideasPendingAction, dashboard.ideasPendingAction],
        ['Rejected: ' + dashboard.ideasRejected, dashboard.ideasRejected]
      ]
    }]
  });
}
