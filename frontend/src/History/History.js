import React from "react";
import styled from "styled-components";
import { useGlobalContext } from "../context/globalContext";

function History() {
  const { transactionHistory } = useGlobalContext();

  const [...history] = transactionHistory();

  return (
    <HistoryStyled>
      <h2>Ostatnie</h2>
      {history.map((item) => {
        const { _id, title, amount, type } = item;
        return (
          <div key={_id} className="history-item">
            <p
              style={{
                color:
                  type === "expense"
                    ? "var(--color-orange)"
                    : "var(--accent-200)",
              }}
            >
              {title}
            </p>

            <p
              style={{
                color:
                  type === "expense"
                    ? "var(--color-orange)"
                    : "var(--accent-200)",
              }}
            >
              {type === "expense"
                ? `-${amount <= 0 ? 0 : amount}`
                : `+${amount <= 0 ? 0 : amount}`}
            </p>
          </div>
        );
      })}
    </HistoryStyled>
  );
}

const HistoryStyled = styled.div`
  display: flex;
  flex-direction: column;
  gap: 1rem;
  background: var(--bg-200);
  border: 1px solid var(--bg-300);
  padding: 20px;
  border-radius: 20px;
  margin-top: 30px;
  .history-item {
    border: 2px solid var(--bg-300);
    box-shadow: 0px 1px 15px rgba(0, 0, 0, 0.06);
    padding: 1rem;
    border-radius: 20px;
    display: flex;
    justify-content: space-between;
    align-items: center;
  }
`;

export default History;
