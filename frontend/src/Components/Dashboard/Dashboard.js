import React, { useEffect } from "react";
import styled from "styled-components";
import { useGlobalContext } from "../../context/globalContext";
import History from "../../History/History";
import { InnerLayout } from "../../styles/Layouts";
import Chart from "../Chart/Chart";

function Dashboard() {
  const {
    totalExpenses,
    incomes,
    expenses,
    totalIncome,
    totalBalance,
    getIncomes,
    getExpenses,
  } = useGlobalContext();

  useEffect(() => {
    getIncomes();
    getExpenses();
  }, []);

  return (
    <DashboardStyled>
      <InnerLayout>
        <div className="wrapper">
          {/* kolumna1 */}
          <div className="column">
            <div className="balance">
              <h2>Budżet</h2>
              <p>{totalBalance()}zł</p>
            </div>
            <div className="history-con">
              <History />
            </div>
          </div>
          {/* kolumna2 */}
          <div className="column">
            <Chart />
            <div className="amount-con">
              <div className="income">
                <h2>Całkowity Przychód</h2>
                <p>{totalIncome()}zł</p>
              </div>
              <div className="expense">
                <h2>Suma Wydatków</h2>
                <p>{totalExpenses()}zł</p>
              </div>
            </div>
          </div>
        </div>
        <div className="wrapper">
          <div className="column">
            <div className="minmax-con">
              <h2 className="salary-title">
                Min <span>Wydatki</span>Maks
              </h2>
              <div className="salary-item">
                <p>{Math.min(...expenses.map((item) => item.amount))}zł</p>
                <p>{Math.max(...expenses.map((item) => item.amount))}zł</p>
              </div>
            </div>
          </div>
          <div className="column">
            <div className="minmax-con">
              <h2 className="salary-title">
                Min <span>Przychody</span>Maks
              </h2>
              <div className="salary-item">
                <p>{Math.min(...incomes.map((item) => item.amount))}zł</p>
                <p>{Math.max(...incomes.map((item) => item.amount))}zł</p>
              </div>
            </div>
          </div>
        </div>
      </InnerLayout>
    </DashboardStyled>
  );
}

const DashboardStyled = styled.div`
  .wrapper{
    display: flex;
    flex-direction:
    justify-content: space-between;

  }
    .column {

      justify-content:space-between;
      width: 50%;
      display: flex;
      flex-direction: column;
      padding: 20px;
    }

    .balance {
      background: linear-gradient(90deg, var(--primary-100) 0%, var(--primary-200) 100%);
      display: flex;
      flex-direction: column;
      padding: 30px 30px 30px 50px;
      border-radius: 20px;
      h2{
        font-size:2.3rem;
      }
      p {
        font-size: 4.6rem;
      }
    }
    
      .amount-con {
        background:var(--bg-200);
        border: 1px solid var(--bg-300);
        padding: 20px;
        display: grid;
        grid-template-columns: repeat(4, 1fr);
        gap: 2rem;
        margin-top: 2rem;
        font-size: .7rem;
        border-radius:20px;
        .income,
        .expense {
          grid-column: span 2;
          p{
            font-size:3.5rem;
            font-weight:700;
          }
        }
        .income{
          p{
            color:var(--accent-200);
          }
        }
        .expense{
          p{
            color:var(--color-orange);
          }
        }
        

      }
    

      .minmax-con {
        margin-top:1.2rem;
        background:var(--bg-200);
        border: 1px solid var(--bg-300);
        padding: 1rem 2rem 1rem 2rem;
        border-radius:20px;
        
        h2 {
          display: flex;
          align-items: center;
          justify-content: space-between;
        }
        .salary-title {
          font-size: 1.5rem;
          span {
            font-size: 1.8rem;
          }
        }
        .salary-item {
          // background: var(--text-200);
          // border: 2px solid var(--bg-300);
          // box-shadow: 0px 1px 15px rgba(0, 0, 0, 0.06);
          padding-top: 1rem;
          border-radius: 20px;
          display: flex;
          justify-content: space-between;
          align-items: center;
          p {
            font-weight: 600;
            font-size: 1.6rem;
          }
        }
      }
`;

export default Dashboard;
