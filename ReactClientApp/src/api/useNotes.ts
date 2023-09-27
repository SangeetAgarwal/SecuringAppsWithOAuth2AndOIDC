import axios from "axios";
import { useQuery } from "react-query";

const config = {
  headers: {
    "X-CSRF": "1",
  },
};

const fetchNotes = async () => {
  const response = await axios("/api/notes", config);
  return response.data;
};

const useNotes = () => {
  const { isLoading, error, data } = useQuery("notes", fetchNotes, {
    retry: false,
  });
  return { isLoading, error, data };
};

export default useNotes;
