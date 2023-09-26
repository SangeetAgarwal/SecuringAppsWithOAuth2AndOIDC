import axios from "axios";
import { useEffect, useState } from "react";
import { useQuery } from "react-query";

interface AuthUser {
  given_name: string | undefined;
  family_name: string | undefined;
  email: string;
  sub: string;
  roles: string[];
}

const config = {
  headers: {
    "X-CSRF": "1",
  },
};

export const fetchClaims = async () => {
  const response = await axios.get("/bff/user", config);
  return response.data;
};

function useClaims() {
  const { isLoading, error, data } = useQuery("claims", fetchClaims, {
    retry: false,
  });
  return { isLoading, error, data };
}

export function useAuthUser() {
  const { isLoading, error, data } = useClaims();
  const claims = data as [{ type: string; value: string }];

  const [user, setUser] = useState<AuthUser | null>(null);

  useEffect(() => {
    if (claims) {
      const given_name = claims.find((c) => c.type === "given_name")?.value ?? "";
      const family_name = claims.find((c) => c.type === "family_name")?.value ?? "";
      const email = claims.find((c) => c.type === "email")?.value ?? "";
      const sub = claims.find((c) => c.type === "sub")?.value ?? "";
      const roles = claims.filter((c) => c.type === "role").map((c) => c.value);
      setUser({ given_name, family_name, email, sub, roles });
    }
  }, [claims]);
  return { isLoading, error, claims, user };
}
