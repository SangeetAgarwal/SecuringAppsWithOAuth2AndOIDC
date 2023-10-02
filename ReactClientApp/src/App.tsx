import useNotes from "./api/useNotes";
import Login from "./components/login";
import Logout from "./components/logout";
import { useAuthUser } from "./open-id/useAuthUser";
export default function App() {
  const authUser = useAuthUser();
  const notes = useNotes();

  console.log(authUser);
  return (
    <>
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
      <div>
        {notes.isLoading ? (
          <div>Loading...</div>
        ) : (
          <div>
            {notes.data && notes.data.length > 0 && <h1>Notes</h1>}
            {notes.data?.map((note: { id: string; title: string; description: string; content: string }) => (
              <div key={note.id}>
                <pre>{JSON.stringify(note, null, 2)}</pre>
              </div>
            ))}
          </div>
        )}
      </div>
    </>
  );
}
