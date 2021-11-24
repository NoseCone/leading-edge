import React from "react";

export const Index = () => {
    const [compPrefix, setCompPrefix] = React.useState("2017-dalby");
    return (
        <div id="content" className="content">
            <p>{compPrefix}</p>
            <div className="tile is-ancestor">
                <div className="tile is-parent">
                    <article className="tile is-child notification is-light">
                        <p className="title">Leading Edge</p>
                        <p className="subtitle">
                            Comps scored with <a href="https://flaretiming.com">Flare Timing</a> and presented with
                            {' '}<a href="https://zaid-ajaj.github.io/Feliz/">Feliz</a>
                        </p>
                    </article>
                </div>
            </div>
            <p>
                Want <a href="https://flaretiming.com/posts/2018-12-19-add-a-comp.html">your comp here</a>?
            </p>
            <div className="tile is-ancestor">
                <div className="tile is-vertical is-5">
                    <div className="tile">
                        <div className="tile is-parent is-vertical">
                            <div className="tile is-child box">
                                <h3>Paragliding</h3>
                                <p />
                                <ul>
                                    <li>
                                        Italian Open
                                        {' '}<a onClick={() => setCompPrefix('2020-italy-open')}>2020</a>
                                    </li>
                                    <li>
                                        Dalmatian
                                        {' '}<a onClick={() => setCompPrefix('2019-dalmatian')}>2019</a>
                                        {' '}<a onClick={() => setCompPrefix('2018-dalmatian')}>2018</a>
                                    </li>
                                    <ul>
                                        <p />
                                    </ul>
                                </ul>
                            </div>
                            <div className="tile is-child box">
                                <h3>Comp Archetypes</h3>
                                <p />
                                <ul>
                                    <li>
                                        <a onClick={() => setCompPrefix('1976-never-land')}>1976 Never Land</a>
                                    </li>
                                    <li>
                                        <a onClick={() => setCompPrefix('1989-lift-lines')}>1989 Lift Lines</a>
                                    </li>
                                    <ul>
                                        <p />
                                    </ul>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="tile is-vertical is-7">
                    <div className="tile">
                        <div className="tile is-parent is-vertical">
                            <div className="tile is-child box">
                                <h3>Hang Gliding</h3>
                                <h5>Oceania</h5>
                                <ul>
                                    <li>
                                        Forbes Flatlands
                                        {' '}<a onClick={() => setCompPrefix('2018-forbes')}>2018</a>
                                        {' '}<a onClick={() => setCompPrefix('2017-forbes')}>2017</a>
                                        {' '}<a onClick={() => setCompPrefix('2016-forbes')}>2016</a>
                                        {' '}<a onClick={() => setCompPrefix('2015-forbes')}>2015</a>
                                        {' '}<a onClick={() => setCompPrefix('2014-forbes')}>2014</a>
                                        {' '} <a onClick={() => setCompPrefix('2012-forbes')}>2012</a>
                                    </li>
                                    <li>
                                        Dalby Big Air <a onClick={() => setCompPrefix('2017-dalby')}>2017</a>
                                    </li>
                                </ul>
                                <h5>Europe</h5>
                                <ul>
                                    <li>
                                        Meduno <a onClick={() => setCompPrefix('2020-meduno')}>2020</a>
                                    </li>
                                    <li>
                                        Tolmezzo <a onClick={() => setCompPrefix('2019-italy')}>2019</a>
                                    </li>
                                </ul>
                                <h5>Americas</h5>
                                <ul>
                                    <li>
                                        Green Swamp Klassic 2016
                                        {' '}<a onClick={() => setCompPrefix('2016-greenswamp')}>Topless</a>
                                        {' '}<a onClick={() => setCompPrefix('2016-greenswamp-sport')}
                                        >Kingposted</a
                                        >
                                    </li>
                                    <li>
                                        Big Spring <a onClick={() => setCompPrefix('2016-big-spring')}>2016</a>
                                    </li>
                                    <li>
                                        QuestAir Open <a onClick={() => setCompPrefix('2016-quest')}>2016</a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>);
}
