import { useRouteError } from "react-router-dom";

export default function ErrorPage() {
  const error = useRouteError();

  return (
    <div className="min-h-screen bg-background flex flex-col items-center justify-center">
      <h1 className="text-4xl font-heading text-error mb-4">¡Ha ocurrido un error!</h1>
      <div className="bg-white rounded-lg shadow p-6 max-w-lg">
        <pre className="text-sm text-error whitespace-pre-wrap">
          {error ? JSON.stringify(error, null, 2) : "No hay información de error disponible."}
        </pre>
      </div>
      <a href="/" className="mt-6 text-primary underline">Volver al inicio</a>
    </div>
  );
}