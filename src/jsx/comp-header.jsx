import React from "react";

export const CompHeader = ({comp, nominals}) => {
    return (
        <div className="container">
            <div className="tile is-ancestor" style={{flexWrap: "wrap"}}>
                <div className="tile">
                    <div className="tile is-parent">
                        <div className="tile is-child box">
                            <p className="title is-3">{comp.compName}</p>
                            <p className="title is-5">{comp.compSlug}</p>
                            <div className="example">
                                <div className="field is-grouped is-grouped-multiline">
                                    <div className="control">
                                        <div className="tags has-addons">
                                            <span className="tag">UTC offset</span>
                                            <span className="tag is-warning">{comp.utcOffset.timeZoneMinutes}</span>
                                        </div>
                                    </div>
                                    <div className="control">
                                        <div className="tags has-addons">
                                            <span className="tag">minimum distance</span>
                                            <span className="tag is-black">{nominals.free}</span>
                                        </div>
                                    </div>
                                    <div className="control">
                                        <div className="tags has-addons">
                                            <span className="tag">nominal distance</span>
                                            <span className="tag is-info">{nominals.distance}</span>
                                        </div>
                                    </div>
                                    <div className="control">
                                        <div className="tags has-addons">
                                            <span className="tag">nominal time</span>
                                            <span className="tag is-success">{nominals.time}</span>
                                        </div>
                                    </div>
                                    <div className="control">
                                        <div className="tags has-addons">
                                            <span className="tag">nominal goal</span>
                                            <span className="tag is-primary">{nominals.goal}</span>
                                        </div>
                                    </div>
                                    <div className="control">
                                        <div className="tags has-addons">
                                            <span className="tag">score-back time</span>
                                            <span className="tag is-danger">{comp.scoreBack}</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}