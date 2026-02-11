import { Component } from '@angular/core';
import { SharedLibraryModule } from 'src/shared-library/shared-library.module';
import { AgChartsModule } from 'ag-charts-angular';
import { AgChartOptions, BarSeriesModule,DonutSeriesModule, GroupedCategoryAxisModule,PieSeriesModule, LegendModule, ModuleRegistry, NumberAxisModule, AllCommunityModule, AreaSeriesModule, CategoryAxisModule, } from 'ag-charts-community';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [SharedLibraryModule, AgChartsModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {

  constructor() {
    ModuleRegistry.registerModules([AllCommunityModule,DonutSeriesModule, BarSeriesModule, GroupedCategoryAxisModule,PieSeriesModule, LegendModule, NumberAxisModule, AreaSeriesModule, CategoryAxisModule
    ]);
  }
  options: AgChartOptions = {
    title: {
      text: "Apple's Revenue by Product Category",
    },
    subtitle: {
      text: "In Billion U.S. Dollars",
    },
    data: [
      {
        quarter: ["2018", "Q1"],
        iphone: 140,
        mac: 16,
        ipad: 14,
        wearables: 12,
        services: 20,
      },
      {
        quarter: ["2018", "Q2"],
        iphone: 124,
        mac: 20,
        ipad: 14,
        wearables: 12,
        services: 30,
      },
      {
        quarter: ["2018", "Q3"],
        iphone: 112,
        mac: 20,
        ipad: 18,
        wearables: 14,
        services: 36,
      },
      {
        quarter: ["2018", "Q4"],
        iphone: 118,
        mac: 24,
        ipad: 14,
        wearables: 14,
        services: 36,
      },
      {
        quarter: ["2019", "Q1"],
        iphone: 124,
        mac: 18,
        ipad: 16,
        wearables: 18,
        services: 26,
      },
      {
        quarter: ["2019", "Q2"],
        iphone: 108,
        mac: 20,
        ipad: 16,
        wearables: 18,
        services: 40,
      },
      {
        quarter: ["2019", "Q3"],
        iphone: 96,
        mac: 22,
        ipad: 18,
        wearables: 24,
        services: 42,
      },
      {
        quarter: ["2019", "Q4"],
        iphone: 104,
        mac: 22,
        ipad: 14,
        wearables: 20,
        services: 40,
      },
    ],
    series: [
      {
        type: "bar",
        xKey: "quarter",
        yKey: "iphone",
        yName: "iPhone",
      },
      {
        type: "bar",
        xKey: "quarter",
        yKey: "mac",
        yName: "Mac",
      },
      {
        type: "bar",
        xKey: "quarter",
        yKey: "ipad",
        yName: "iPad",
      },
      {
        type: "bar",
        xKey: "quarter",
        yKey: "wearables",
        yName: "Wearables",
      },
      {
        type: "bar",
        xKey: "quarter",
        yKey: "services",
        yName: "Services",
      },
    ],
    axes: {
      x: {
        type: "grouped-category",
        label: { rotation: 0 },
        depthOptions: [{}, { label: { fontWeight: "bold" } }],
      },
    },
  };

   options1: AgChartOptions = {
      title: {
        text: "Sales by Month",
      },
      data: this.getData1(),
      series: [
        {
          type: "area",
          xKey: "month",
          yKey: "subscriptions",
          yName: "Subscriptions",
          stroke: "blue",
          strokeWidth: 3,
          lineDash: [3, 4],
          fill: "lightBlue",
        },
        {
          type: "area",
          xKey: "month",
          yKey: "services",
          yName: "Services",
          stroke: "red",
          strokeWidth: 3,
          fill: "pink",
          marker: {
            enabled: true,
            fill: "red",
          },
        },
        {
          type: "area",
          xKey: "month",
          yKey: "products",
          yName: "Products",
          stroke: "green",
          strokeWidth: 3,
          fill: "lightGreen",
          label: {
            enabled: true,
            fontWeight: "bold",
            formatter: ({ value }) => value.toFixed(0),
          },
        },
      ],
    };

    options2: AgChartOptions = {
      data: this.getData2(),
      title: {
        text: "Portfolio Composition",
      },
      series: [
        {
          type: "pie",
          angleKey: "amount",
          calloutLabelKey: "asset",
          sectorLabelKey: "amount",
          sectorLabel: {
            color: "white",
            fontWeight: "bold",
            formatter: ({ value }) => `$${(value / 1000).toFixed(0)}K`,
          },
        },
      ],
    };
    options3:AgChartOptions = {
      data: this.getData3(),
      title: {
        text: "Portfolio Composition",
      },
      subtitle: {
        text: "Versus Previous Year",
      },
      series: [
        {
          type: "donut",
          title: {
            text: "Previous Year",
          },
          calloutLabelKey: "asset",
          legendItemKey: "asset",
          angleKey: "previousYear",
          outerRadiusRatio: 1,
          innerRadiusRatio: 0.9,
        },
        {
          type: "donut",
          title: {
            text: "Current Year",
          },
          legendItemKey: "asset",
          showInLegend: false,
          angleKey: "currentYear",
          outerRadiusRatio: 0.6,
          innerRadiusRatio: 0.2,
        },
      ],
    };
    getData1(){
      return [
    { month: "Jan", subscriptions: 222, services: 250, products: 200 },
    { month: "Feb", subscriptions: 240, services: 255, products: 210 },
    { month: "Mar", subscriptions: 280, services: 245, products: 195 },
    { month: "Apr", subscriptions: 300, services: 260, products: 205 },
    { month: "May", subscriptions: 350, services: 235, products: 215 },
    { month: "Jun", subscriptions: 420, services: 270, products: 200 },
    { month: "Jul", subscriptions: 300, services: 255, products: 225 },
    { month: "Aug", subscriptions: 270, services: 305, products: 210 },
    { month: "Sep", subscriptions: 260, services: 280, products: 250 },
    { month: "Oct", subscriptions: 385, services: 250, products: 205 },
    { month: "Nov", subscriptions: 320, services: 265, products: 215 },
    { month: "Dec", subscriptions: 330, services: 255, products: 220 },
  ];
    }
    getData2() {
  return [
    { asset: "Stocks", amount: 60000 },
    { asset: "Bonds", amount: 40000 },
    { asset: "Cash", amount: 7000 },
    { asset: "Real Estate", amount: 5000 },
    { asset: "Commodities", amount: 3000 },
  ];
}
getData3() {
  return [
    { asset: "Stocks", previousYear: 70000, currentYear: 40000 },
    { asset: "Bonds", previousYear: 30000, currentYear: 60000 },
    { asset: "Cash", previousYear: 5000, currentYear: 7000 },
    { asset: "Real Estate", previousYear: 8000, currentYear: 5000 },
    { asset: "Commodities", previousYear: 4500, currentYear: 3000 },
  ];
}
}
