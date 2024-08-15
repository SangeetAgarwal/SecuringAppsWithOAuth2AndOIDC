import { useAuthUser } from "../../open-id/useAuthUser";

export default function Login() {
  const authUser = useAuthUser();
  const bffLogoutUrl = authUser?.user?.bffLogoutUrl;
  return (
    <a href={bffLogoutUrl ? bffLogoutUrl : "/bff/logout"} style={{ color: "white" }}>
      Logout
    </a>
  );
}
