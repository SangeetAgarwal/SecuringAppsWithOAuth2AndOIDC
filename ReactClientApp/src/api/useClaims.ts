import axios from "axios";
import { useQuery } from "react-query";

const config = {
  headers: {
    "X-CSRF": "1",
  },
};

const fetchAdditionalClaims = async () => {
  const response = await axios("/api/claimsFromAPI", config);
  return response.data;
};

const useClaims = () => {
  const { isLoading, error, data } = useQuery("claimsFromAPI", fetchAdditionalClaims, {
    retry: false,
  });
  return { isLoading, error, data };
};

export default useClaims;
