import "./Header.css";
import { useAuthUser } from "../../open-id/useAuthUser";
import Logout from "../logout";
import Login from "../login";
const Header = () => {
  const authUser = useAuthUser();
  return (
    <header className="header">
      <div className="logo">React bff client app</div>
      <nav className="nav">
      </nav>
      <div className="profile">
        {authUser.user ? (
          <>
            <Logout></Logout>
          </>
        ) : (
          <>
            <Login></Login>
          </>
        )}
      </div>
    </header>
  );
};

export default Header;
