import { useAuthUser } from "../../open-id/useAuthUser";

export default function Login() {
  const authUser = useAuthUser();
  const bffLogoutUrl = authUser?.user?.bffLogoutUrl;
  return (
    <a href={bffLogoutUrl ? bffLogoutUrl : "/bff/logout"} style={{ top: 0, position: "fixed", right: 0, margin: "10px" }}>
      Logout
    </a>
  );
}
