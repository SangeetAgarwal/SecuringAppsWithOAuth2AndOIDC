import Login from "./components/login";
import Logout from "./components/logout";
import { useAuthUser } from "./open-id/useAuthUser";
export default function App() {
  const authUser = useAuthUser();
  console.log(authUser);
  return (
    <div>
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
  );
}
