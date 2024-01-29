import React, { useState, useMemo } from "react";
import styled from "styled-components";
import { MainLayout } from "./styles/Layouts";
import Navigation from "./Components/Navigation/Navigation";
import Dashboard from "./Components/Dashboard/Dashboard";
import Income from "./Components/Income/Income";
import Expenses from "./Components/Expenses/Expenses";
import { useGlobalContext } from "./context/globalContext";
import Login from "./Components/Login/Login";
import Register from "./Components/Register/Register";

function App() {
  const [active, setActive] = useState(1);

  const { isLogin } = useGlobalContext();

  const displayData = () => {
    if (isLogin()) {
      switch (active) {
        case 1:
          return <Dashboard />;
        case 2:
          return <Dashboard />;
        case 3:
          return <Income />;
        case 4:
          return <Expenses />;
        default:
          return <Dashboard />;
      }
    } else {
      switch (active) {
        case 5:
          return <Login />;
        case 6:
          return <Register />;
        default:
          return <Login />;
      }
    }
  };

  return (
    <AppStyled className="App">
      <MainLayout>
        <Navigation active={active} setActive={setActive} />
        <main>{displayData()}</main>
      </MainLayout>
    </AppStyled>
  );
}

const AppStyled = styled.div`
  height: 100vh;
  background: var(--bg-100);
  position: relative;
  main {
    flex: 1;
    // background: rgba(252, 246, 249, 0.78);
    // border: 3px solid #ffffff;
    backdrop-filter: blur(4.5px);
    border-radius: 32px;
    overflow-x: hidden;
    &::-webkit-scrollbar {
      width: 0;
    }
  }
`;

export default App;
