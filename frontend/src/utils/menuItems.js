import { dashboard, expenses, transactions, trend } from "../utils/Icons";

export const menuItems = [
  {
    id: 1,
    title: "Strona Główna",
    icon: dashboard,
    link: "/dashboard",
  },
  // {
  //     id: 2,
  //     title: "View Transactions",
  //     icon: transactions,
  //     link: "/dashboard",
  // },
  {
    id: 3,
    title: "Przychody",
    icon: trend,
    link: "/dashboard",
  },
  {
    id: 4,
    title: "Wydatki",
    icon: expenses,
    link: "/dashboard",
  },
];
