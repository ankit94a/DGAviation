import { Component, OnInit } from '@angular/core';
import { SharedLibraryModule } from 'src/shared-library/shared-library.module';
import { AgChartsModule } from 'ag-charts-angular';
import { AgChartOptions, BarSeriesModule, DonutSeriesModule, GroupedCategoryAxisModule, PieSeriesModule, LegendModule, ModuleRegistry, NumberAxisModule, AllCommunityModule, AreaSeriesModule, CategoryAxisModule, } from 'ag-charts-community';
import { StatCard } from 'src/shared-library/models/dashboard.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [SharedLibraryModule, AgChartsModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  cards: StatCard[] = [];
  chartOptions: AgChartOptions;
  lineChartOptions: AgChartOptions;
  pieChartOptions: AgChartOptions;
  constructor() {
    ModuleRegistry.registerModules([AllCommunityModule, DonutSeriesModule, BarSeriesModule, GroupedCategoryAxisModule, PieSeriesModule, LegendModule, NumberAxisModule, AreaSeriesModule, CategoryAxisModule
    ]);
    this.cards = [
      {
        title: 'Total Candidate',
        value: 350,
        icon: 'download',
        colorClass: 'bg-success',
        meta: 'This year',
        positive: true
      },
      {
        title: 'New Registered',
        value: 80,
        icon: 'upload',
        colorClass: 'bg-primary',
        meta: 'This year',
        positive: false
      },
      {
        title: 'Verified',
        value: 155,
        icon: 'block',
        colorClass: 'bg-pink',
        meta: 'This year',
        positive: false
      },
      {
        title: 'Under Verification',
        value: 99,
        icon: 'inventory',
        colorClass: 'bg-dark',
        meta: 'Available now',
        positive: true
      }
    ];
  }
  ngOnInit(): void {
    this.initChart()
    this.initLineChart();
    this.initPieChart();
  }
  initPieChart() {
    this.pieChartOptions = {
      title: {
        text: 'Candidate Status Distribution',
      },

      data: this.getPieData(),

      series: [
        {
          type: 'pie',
          angleKey: 'value',
          calloutLabelKey: 'status',   // ✅ correct property
          sectorLabelKey: 'value',     // optional (shows value inside)
        },
      ],

      legend: {
        position: 'bottom',
      },
    };
  }

  initLineChart() {
    this.lineChartOptions = {
      title: {
        text: 'Monthly Registration Trend',
      },

      data: this.getLineData(),

      series: [
        {
          type: 'line',
          xKey: 'month',
          yKey: 'value',
          yName: 'Registrations',
          marker: {
            enabled: true,
          },
        },
      ],

      legend: {
        position: 'bottom',
      },
    };
  }

  initChart() {
    this.chartOptions = {
      title: {
        text: 'Monthly Candidate Movement',
      },

      data: this.getData1(),
      series: [
        {
          type: 'bar',
          xKey: 'month',
          yKey: 'registered',
          yName: 'Registered',
          cornerRadius: 6,
          fill: '#22c55e',

        },
        {
          type: 'bar',
          xKey: 'month',
          yKey: 'verified',
          yName: 'Verified',
          cornerRadius: 6,
          fill: '#3b82f6',

        },
        {
          type: 'bar',
          xKey: 'month',
          yKey: 'pending',
          yName: 'Pending',
          cornerRadius: 6,
          fill: '#ef4444',

        },
      ],
      legend: {
        position: 'bottom',
      },
      axes: {
        x: {
          type: "grouped-category",
          label: { rotation: 0 },
          depthOptions: [{}, { label: { fontWeight: "bold" } }],
        },
      },
    };
  }

  getPieData() {
    return [
      { status: 'Registered', value: 420 },
      { status: 'Verified', value: 350 },
      { status: 'Pending', value: 180 },
      { status: 'Rejected', value: 90 },
    ];
  }

  getLineData() {
    return [
      { month: 'Jan', value: 220 },
      { month: 'Feb', value: 240 },
      { month: 'Mar', value: 280 },
      { month: 'Apr', value: 300 },
      { month: 'May', value: 350 },
      { month: 'Jun', value: 420 },
      { month: 'Jul', value: 300 },
      { month: 'Aug', value: 270 },
      { month: 'Sep', value: 260 },
      { month: 'Oct', value: 385 },
      { month: 'Nov', value: 320 },
      { month: 'Dec', value: 330 },
    ];
  }

  getData1() {
    return [
      { month: "Jan", registered: 222, verified: 250, pending: 200 },
      { month: "Feb", registered: 240, verified: 255, pending: 210 },
      { month: "Mar", registered: 280, verified: 245, pending: 195 },
      { month: "Apr", registered: 300, verified: 260, pending: 205 },
      { month: "May", registered: 350, verified: 235, pending: 215 },
      { month: "Jun", registered: 420, verified: 270, pending: 200 },
      { month: "Jul", registered: 300, verified: 255, pending: 225 },
      { month: "Aug", registered: 270, verified: 305, pending: 210 },
      { month: "Sep", registered: 260, verified: 280, pending: 250 },
      { month: "Oct", registered: 385, verified: 250, pending: 205 },
      { month: "Nov", registered: 320, verified: 265, pending: 215 },
      { month: "Dec", registered: 330, verified: 255, pending: 220 },
    ];
  }

}
