import "./EventsHero.css";

export function EventsHero() {
  return (
    <section className="events-hero">
      <div className="events-hero__content">
        <span className="events-hero__eyebrow">
          LIVE THE MOMENT
        </span>

        <h1>
          Find your next
          <span> unforgettable night.</span>
        </h1>

        <p>
          Discover concerts, festivals, shows and experiences
          worth getting out of the house for.
        </p>

        <div className="events-hero__chips">
          <span>♫ Live music</span>
          <span>✦ Big moments</span>
          <span>♪ Good company</span>
        </div>
      </div>

      <div className="events-hero__art" aria-hidden="true">
        <div className="events-hero__disc">
          <div className="events-hero__disc-hole" />
        </div>

        <div className="events-hero__note events-hero__note--one">
          ♪
        </div>

        <div className="events-hero__note events-hero__note--two">
          ♫
        </div>

        <div className="events-hero__spark events-hero__spark--one">
          ✦
        </div>

        <div className="events-hero__spark events-hero__spark--two">
          ✦
        </div>
      </div>
    </section>
  );
}