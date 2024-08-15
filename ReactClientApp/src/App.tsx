import useClaims from "./api/useClaims";
import useNotes from "./api/useNotes";
import { useAuthUser } from "./open-id/useAuthUser";
import Header from "./components/header/index.tsx";

export default function App() {
  const authUser = useAuthUser();
  const notes = useNotes();
  const response = useClaims();
  console.log("claims", response);

  return (
    <>
      <Header />
      <div>
        {notes.isLoading && authUser.user ? (
          <div className="flex items-center justify-center h-16">
            <div className="animate-spin rounded-full h-8 w-8 border-t-2 border-b-2 border-blue-500"></div>
            <span className="ml-4 text-blue-500 font-semibold">Loading...</span>
          </div>
        ) : (
          authUser.user && (
            <>
              <div className="bg-white rounded-lg p-5 m-5 shadow-md">
                {notes.data && notes.data.length > 0}
                <h1 className="text-lg font-bold text-gray-800 mb-5 border-b-2 border-gray-300 pb-2.5">Notes</h1>
                <div className="grid grid-cols-[1fr_2fr] gap-2.5 font-bold border-b border-gray-300 pb-2.5 mb-2.5 text-gray-500">
                  <div>Title</div>
                  <div>Description</div>
                </div>

                {notes.data?.map((note: { id: string; title: string; description: string; content: string }) => (
                  <div key={note.id} className="grid grid-cols-[1fr_2fr] gap-2.5 border-b border-gray-300 pb-2.5 mb-2.5">
                    <div>{note.title}</div>
                    <div>{note.description}</div>
                  </div>
                ))}
              </div>
              <div>
                {response.data && response.data.claims && response.data.claims.length > 0 && (
                  <>
                    <div className="bg-white rounded-lg p-5 m-5 shadow-md">
                      <h1 className="text-lg font-bold text-gray-800 mb-5 border-b-2 border-gray-300 pb-2.5">Claims</h1>
                      <div className="grid grid-cols-[1fr_2fr] gap-2.5 font-bold border-b border-gray-300 pb-2.5 mb-2.5 text-gray-500">
                        <div>Type</div>
                        <div>Value</div>
                      </div>
                      {response.data.claims.map((claim: { type: string; value: string }) => (
                        <div key={claim.type} className="grid grid-cols-[1fr_2fr] gap-2.5 border-b border-gray-300 pb-2.5 mb-2.5">
                          <div>{claim.type}</div>
                          <div>{claim.value}</div>
                        </div>
                      ))}
                    </div>
                  </>
                )}
              </div>
            </>
          )
        )}
      </div>
    </>
  );
}
