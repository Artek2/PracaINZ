import React, { useState } from "react";
import styled from "styled-components";
import avatar from "../../img/avatar.png";
import { signout } from "../../utils/Icons";
import { menuItems } from "../../utils/menuItems";
import { useGlobalContext } from "../../context/globalContext";

function Navigation({ active, setActive }) {
  const { isLogin, totalBalance, logout, getName } = useGlobalContext();

  return (
    <NavStyled>
      {isLogin() && (
        <>
          <div className="user-con">
            <img src={avatar} alt="" />
            <div className="text">
              <h2>{getName()}</h2>
              <p>Budżet: {totalBalance()}</p>
            </div>
          </div>
          <ul className="menu-items">
            {menuItems.map((item) => {
              return (
                <li
                  onClick={() => setActive(item.id)}
                  className={active === item.id ? "active" : ""}
                >
                  {item.icon}
                  <span>{item.title}</span>
                </li>
              );
            })}
          </ul>
          <div className="menu-items log-out-btn">
            <li onClick={() => logout()}>{signout} Wyloguj</li>
          </div>
        </>
      )}
      {!isLogin() && (
        <div className="menu-items">
          <li
            onClick={() => setActive(5)}
            className={active === 5 ? "active" : ""}
          >
            {" "}
            Logowanie
          </li>
          <li
            onClick={() => setActive(6)}
            className={active === 6 ? "active" : ""}
          >
            {" "}
            Rejestracja
          </li>
        </div>
      )}
    </NavStyled>
  );
}

const NavStyled = styled.nav`
  padding: 2rem 1.5rem;
  width: 374px;
  height: 100%;
  border-right: 1px solid #e0e0e0;
  backdrop-filter: blur(4.5px);
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  gap: 2rem;
  color: var(--text-100);
  .user-con {
    height: 100px;
    display: flex;
    align-items: center;
    gap: 1rem;
    img {
      width: 80px;
      height: 80px;
      border-radius: 50%;
      object-fit: cover;
      background: var(--bg-300);
      border: 2px solid var(--text-100);
      padding: 0.2rem;
      box-shadow: 0px 1px 17px rgba(0, 0, 0, 0.06);
    }
  }

  .menu-items {
    flex: 1;
    display: flex;
    flex-direction: column;
    li {
      display: grid;
      grid-template-columns: 40px auto;
      align-items: center;
      margin: 0.6rem 0;
      font-weight: 500;
      cursor: pointer;
      transition: all 0.4s ease-in-out;

      padding-left: 1rem;
      position: relative;
      i {
        font-size: 1.4rem;
        transition: all 0.4s ease-in-out;
      }
    }
  }
  .log-out-btn {
    position: absolute;
    bottom: 0;
  }

  .active {
    color: var(--text-200);
    i {
      color: var(--text-200);
    }
    &::before {
      content: "";
      position: absolute;
      left: 0;
      top: 0;
      width: 4px;
      height: 100%;
      background: var(--primary-100);
      border-radius: 0 10px 10px 0;
    }
  }
`;

export default Navigation;
